using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/schedule/{category}")]
    public async Task<ActionResult<string>> GetSchedule(string designId, string elementsfilter)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetSchedule($designId: ID!, $elementsfilter: String!){
                  elements (designId: ,
                                   filter: { query: $elementsfilter}
                    ) {
                      results{
                        id
                        name
                        properties {
                           results{
                              name
                              value
                              display value
                               propertyDefinition{
                                units
                            }
                          }
                        }
                references{
                    results {
                        name
                        value {
                           properties{
                               results{
                                 name
                                 value
                                display value
                                propertyDefinition{
                                 units
                                }
                             }
                           }
                        }.  
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