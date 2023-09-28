using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECCIMGraphQLController : ControllerBase
{
	[HttpGet("/api/graphql/designs/{designId}/version/{versionNumber}")]
	public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, int versionNumber, string cursor)
	{
		var properties = new GraphQLRequest
		{
			Query = @"
				query getVersionProperties($designId: ID!, $versionNumber: Int!){
					aecDesignByVersionNumber(designId:$designId , versionNumber:$versionNumber ){
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
									propertyDefinition{
										id
										name
										units
									}
								}
							}
						}
					}
				}
			}",
			Variables = new
			{
				designId = designId,
				versionNumber = versionNumber
			}
		};
		if (!String.IsNullOrWhiteSpace(cursor))
		{
			properties.Query = $@"
			query getVersionProperties($designId: ID!, $versionNumber: Int!){{
					aecDesignByVersionNumber(designId:$designId , versionNumber:$versionNumber, pagination:{{cursor:""{cursor}""}} ){{
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
									propertyDefinition{{
										id
										name
										units
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