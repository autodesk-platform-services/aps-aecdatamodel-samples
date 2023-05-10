using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("projects/{projectId}/properties")]
    public async Task<ActionResult<string>> GetProperties(string projectId)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
			    query getDesignValidation($projectId: ID!) {
            aecDesignsByProject(projectId: $projectId) {
              pagination{
                pageSize
                cursor
              }
    results{
      name
      id
			propertyDefinitions{
				results{
					id
					name
					description
					specification
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