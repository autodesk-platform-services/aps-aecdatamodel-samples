using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;
using Microsoft.AspNetCore.Authentication;

public partial class AECDMGraphQLController : ControllerBase
{
    [HttpGet("hubs")]
    public async Task<ActionResult<string>> GetHubs(string? cursor, string? regionHeader)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query {
                    hubs {
                        pagination {
                            cursor
                        }
                        results {
                            id
                            name
                        }
                    }
                }",
        };
        if (!String.IsNullOrWhiteSpace(cursor))
        {
            properties.Query = $@"
                query {{
                    hubs (pagination:{{cursor:""{cursor}""}}){{
                        pagination {{
                           cursor
                        }}
                        results {{
                            id
                            name
                        }}
			        }}
                }}";
        }

        return await Query(properties, regionHeader);
    }

    [HttpGet("hubs/{hubid}/projects")]
    public async Task<ActionResult<string>> GetProjects(string hubId, string? cursor, string? regionHeader)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
			    query GetProjects ($hubId: ID!) {
			        projects (hubId: $hubId) {
                        pagination {
                           cursor
                        }
                        results {
                            id
                            name
                        }
			        }
			    }",
            Variables = new
            {
                hubId = hubId
            }
        };
        if (!String.IsNullOrWhiteSpace(cursor))
        {
            properties.Query = $@"
                query GetProjects ($hubId: ID!) {{
			        projects (hubId: $hubId, pagination:{{cursor:""{cursor}""}}) {{
                        pagination {{
                           cursor
                        }}
                        results {{
                            id
                            name
                        }}
			        }}
			    }}";
        }

        return await Query(properties, regionHeader);
    }
}