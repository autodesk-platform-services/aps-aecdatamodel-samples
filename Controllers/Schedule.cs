using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/schedule/{category}")]
    public async Task<ActionResult<string>> GetSchedule(string designId, string category)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetDesignEntitiesByCategory ($designId: ID) { 
                    designEntities(
                    filter: {designId: $designId, classificationFilter: {category: ""$category""}}
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
                    }".Replace("$category", category),
            Variables = new
            {
                designId = designId
            }
        };

        return await Query(properties);
    }
}