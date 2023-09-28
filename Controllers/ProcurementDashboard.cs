using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECCIMGraphQLController : ControllerBase
{
	[HttpGet("designs/{designId}/procurement")]
	public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, string elementsfilter, string referencefilter, string? cursor)
	{
		var properties = new GraphQLRequest
		{
			Query = @"
			query GetFurnitureProcurement($designId: ID!, $elementsfilter: String!, $referencefilter: String!) {
				elements(designId: $designId, filter: { query: $elementsfilter}) {
					pagination {
						pageSize
						cursor
					}
					results {
						id
						name
						properties {
							results {
								name
								value
							}
						}
						referencedBy (name: ""Level"", filter: { query: $referencefilter}) {
							results {
								name
								properties {
									results {
										name
										value
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
				elementsfilter = elementsfilter,
				referencefilter = referencefilter
			}
		};
		if (!String.IsNullOrWhiteSpace(cursor))
		{
			properties.Query = $@"
			query GetFurnitureProcurement($designId: ID!, $elementsfilter: String!, $referencefilter: String!) {{
				elements(designId: $designId, filter: {{ query: $elementsfilter}}, pagination:{{cursor:""{cursor}""}}) {{
					pagination {{
						pageSize
						cursor
					}}
					results {{
						id
						name
						properties {{
							results {{
								name
								value
							}}
						}}
						referencedBy (name: ""Level"", filter: {{ query: $referencefilter}}) {{
							results {{
								name
								properties {{
									results {{
										name
										value
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