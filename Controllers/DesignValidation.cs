using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("hubs/{hubid}/projects/{projectId}/properties")]
    public async Task<ActionResult<string>> GetProperties(string projectId)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
			    query aecdesigns ($projectId: ID!) {
                  aecDesignsByProject(
                      projectId: $projectId}
                  ) {
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
                projectId = projectId
            }
        };

        return await Query(properties);
    }
}