using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
    [HttpGet("projects/{projectId}/elementgroups")]
    public async Task<ActionResult<string>> GetElementGroups(string projectId, string? cursor, string? regionHeader)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetElementGroupsByProject($projectId: ID!) {
                    elementGroupsByProject(projectId: $projectId) {
                        pagination {
                            cursor
                        }
                        results{
                            name
                            id
                        }
                    }
                }",
            Variables = new
            {
                projectId = projectId
            }
        };
        if (!String.IsNullOrWhiteSpace(cursor))
        {
            properties.Query = $@"
                query GetElementGroupsByProject($projectId: ID!) {{
                    elementGroupsByProject(projectId: $projectId, pagination:{{cursor:""{cursor}""}}) {{
                        pagination {{
                            cursor
                        }}
                        results{{
                            name
                            id
                        }}
                    }}
                }}";
        }

        return await Query(properties, regionHeader);
    }
}