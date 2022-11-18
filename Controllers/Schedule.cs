using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    // Refer to AECCIMGraphQL class for other methods

    [HttpGet("designs/{designId}/schedule/{category}")]
    public async Task<ActionResult<string>> GetSchedule(string designId, string category)
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
                                description
                                groupName
                                name
                                readOnly
                                specification
                                type
                                units
                            }
                        }
                    }
                    referencedBy {
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
                                    description
                                    groupName
                                    name
                                    readOnly
                                    specification
                                    type
                                    units
                                }
                            }
                        }
                        }
                    }
                    references {
                        results {
                        id
                        name
                        }
                    }
                    }
                }
                }".Replace("$designId", designId).Replace("$category", category),
        };

        return await Query(properties);
    }
}