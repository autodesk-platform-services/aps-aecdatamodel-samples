using System;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using GraphQL;
using Newtonsoft.Json.Linq;

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