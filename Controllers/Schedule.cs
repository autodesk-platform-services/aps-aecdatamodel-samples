using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/schedule/{elementsfilter}")]
    public async Task<ActionResult<string>> GetSchedule(string designId, string elementsfilter)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetSchedule($designId: ID!, $elementsfilter: String!){
	elements (designId: $designId,filter: { query: $elementsfilter}){
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
					displayValue
					propertyDefinition{
						units
					}
				}
			}
			references{
				results{
					name
					value{
						properties{
							results{
								name
								value
								displayValue
								propertyDefinition{
									units
								}
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
                designId = designId,
                elementsfilter = elementsfilter
            }
        };

        return await Query(properties);
    }
}