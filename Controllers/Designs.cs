using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("hubs/{hubid}/projects/{projectId}/designs")]
    public async Task<ActionResult<string>> GetDesigns(string hubId, string projectId)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetDesigns {
                project(
                    hubId: ""$hubId""
                    projectId: ""$projectId""
                ) {
                    id
                    name
                    folders {
                    results {
                        id
                        name
                        items {
                        results {
                            id
                            name
                            __typename
                            ... on Folder {
                            items {
                                results {
                                name
                                id
                                __typename
                                extensionType
                                ... on BasicFile {
                                    designId
                                    name
                                }
                                }
                            }
                            }
                        }
                        }
                    }
                    }
                }
                }".Replace("$hubId", hubId).Replace("$projectId", projectId),
        };

        return await Query(properties);
    }
}