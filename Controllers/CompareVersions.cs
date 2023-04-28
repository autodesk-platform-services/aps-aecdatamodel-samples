using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("/api/graphql/designs/{designId}/version/{versionNumber}")]
    public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, string versionNumber)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
            query getVersionProperties($designId: String!, $versionNumber: String!){
              aecDesignByVersionNumber(designId:$designId , versionNumber:$versionNumber ){
              name
              elements {
                results {
                 id
                 name
                 properties {
                      results {
                       name
                        value
                        propertyDefinition {
                             id
                             name
                             unit
                                  }
                           }
                         }
                      }
                   }
                 }
            }",
            Variables = new
            {
                designId = designId,
                versionNumber = versionNumber
            }
        };

        return await Query(properties);
    }
}