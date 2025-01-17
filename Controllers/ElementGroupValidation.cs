using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
  [HttpGet("projects/{projectId}/properties")]
  public async Task<ActionResult<string>> GetProperties(string projectId, string? cursor, string? regionHeader)
  {
    var properties = new GraphQLRequest
    {
      Query = @"
			    query getElementGroupValidation($projectId: ID!) {
            elementGroupsByProject(projectId: $projectId, pagination:{limit:10}) {
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
    if (!String.IsNullOrWhiteSpace(cursor))
    {
      properties.Query = $@"
      query getElementGroupValidation($projectId: ID!) {{
        elementGroupsByProject(projectId: $projectId, pagination:{{cursor:""{cursor}"", limit:10}}) {{
          pagination{{
            pageSize
            cursor
          }}
          results{{
            name
            id
            propertyDefinitions{{
              results{{
                id
                name
                description
                specification
              }}
            }}
          }}
        }}
      }}";
    }

    return await Query(properties, regionHeader);
  }
}