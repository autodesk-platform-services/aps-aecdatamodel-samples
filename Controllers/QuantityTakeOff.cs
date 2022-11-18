using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    // Refer to AECCIMGraphQL class for other methods
    
    [HttpGet("designs/{designId}/takeoff/{category}")]
    public async Task<ActionResult<string>> GetQuantityTakeOff(string designId, string category)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                {
                designEntities(
                    filter: {designId: ""$designId"", classificationFilter: {category: ""$category""}}
                ) {
                    pagination {
                        cursor
                        limit
                    }
                    results {
                        id
                        classification {
                            category
                        }
                        properties {
                            results {
                                displayValue
                                name
                                value
                                propertyDefinition {
                                    type
                                    units
                                }
                            }
                        }
                    }
                }
                }".Replace("$designId", designId).Replace("$category", category),
        };

        return await Query(properties);
    }
}