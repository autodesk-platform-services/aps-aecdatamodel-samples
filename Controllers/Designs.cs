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
                query GetDesigns ($hubId: String!, $projectId: String!) {
                    project(
                        hubId: $hubId
                        projectId: $projectId
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
                                    ... on BasicFile {
                                            designId
                                            name
                                        }
                                    }
                                }
                            }
                        }
                    }
                }",
            Variables = new
            {
                hubId = hubId,
                projectId = projectId
            }
        };

        return await Query(properties);
    }
}