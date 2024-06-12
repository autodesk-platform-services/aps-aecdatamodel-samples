using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
	[HttpGet("/api/graphql/elementgroups/{elementGroupId}/version/{versionNumber}")]
	public async Task<ActionResult<string>> GetFurnitureProcurement(string elementGroupId, int versionNumber, string cursor)
	{
		var properties = new GraphQLRequest
		{
			Query = @"
				query getVersionProperties($elementGroupId: ID!, $versionNumber: Int!){
					elementGroupByVersionNumber(elementGroupId:$elementGroupId , versionNumber:$versionNumber ){
					name
					elements{
						pagination{
							pageSize
							cursor
						}
						results{
							id
							name
							properties{
								results{
									name
									value
									definition{
										id
										name
										units{
											name
										}
									}
								}
							}
						}
					}
				}
			}",
			Variables = new
			{
				elementGroupId = elementGroupId,
				versionNumber = versionNumber
			}
		};
		if (!String.IsNullOrWhiteSpace(cursor))
		{
			properties.Query = $@"
			query getVersionProperties($elementGroupId: ID!, $versionNumber: Int!){{
					elementGroupByVersionNumber(elementGroupId:$elementGroupId , versionNumber:$versionNumber, pagination:{{cursor:""{cursor}""}} ){{
					name
					elements{{
						pagination{{
							pageSize
							cursor
						}}
						results{{
							id
							name
							properties{{
								results{{
									name
									value
									definition{{
										id
										name
										units{{
											name
										}}
									}}
								}}
							}}
						}}
					}}
				}}
			}}";
		}

		return await Query(properties);
	}
}