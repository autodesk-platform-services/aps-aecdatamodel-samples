using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("projects/{projectId}/designs")]
    public async Task<ActionResult<string>> GetDesigns(string projectId, string? cursor)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetDesignsByProject($projectId: ID!) {
                    aecDesignsByProject(projectId: $projectId) {
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
                query GetDesignsByProject($projectId: ID!) {{
                    aecDesignsByProject(projectId: $projectId, pagination:{{cursor:""{cursor}""}}) {{
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

        return await Query(properties);
    }
}