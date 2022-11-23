using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("hubs/{hubid}/projects/{projectId}/properties")]
    public async Task<ActionResult<string>> GetProperties(string hubId, string projectId)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
			    query GetProperties ($projectId: ID!, $hubId: ID!) {
                    designs(filter: {projectId: $projectId, hubId: $hubId }) {
                        results {
                            id
                            propertyDefinitions {
                                results {
                                    type
                                    name
                                    description
                                    specification
                                    units
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