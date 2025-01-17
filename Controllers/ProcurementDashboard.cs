using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
	[HttpGet("elementgroups/{elementGroupId}/procurement")]
	public async Task<ActionResult<string>> GetFurnitureProcurement(string elementGroupId, string elementsfilter, string referencefilter, string? cursor, string? regionHeader)
	{
		var properties = new GraphQLRequest
		{
			Query = @"
			query GetFurnitureProcurement($elementGroupId: ID!, $elementsfilter: String!, $referencefilter: String!) {
				elementsByElementGroup(elementGroupId: $elementGroupId, filter: { query: $elementsfilter}) {
					pagination {
						pageSize
						cursor
					}
					results {
						id
						name
						properties (filter: {names: [""Element Name""]}){
							results {
								name
								value
							}
						}
						referencedBy (name: ""Level"", filter: { query: $referencefilter}) {
							results {
								name
								properties (filter: {names: [""Family Name"", ""Element Name""]}) {
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
				elementGroupId = elementGroupId,
				elementsfilter = elementsfilter,
				referencefilter = referencefilter
			}
		};
		if (!String.IsNullOrWhiteSpace(cursor))
		{
			properties.Query = $@"
			query GetFurnitureProcurement($elementGroupId: ID!, $elementsfilter: String!, $referencefilter: String!) {{
				elementsByElementGroup(elementGroupId: $elementGroupId, filter: {{ query: $elementsfilter}}, pagination:{{cursor:""{cursor}""}}) {{
					pagination {{
						pageSize
						cursor
					}}
					results {{
						id
						name
						properties (filter: {{names: [""Element Name""]}}) {{
							results {{
								name
								value
							}}
						}}
						referencedBy (name: ""Level"", filter: {{ query: $referencefilter}}) {{
							results {{
								name
								properties (filter: {{names: [""Family Name"", ""Element Name""]}}) {{
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

		return await Query(properties, regionHeader);
	}
}