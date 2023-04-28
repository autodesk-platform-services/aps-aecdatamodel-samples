using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/procurement")]
    public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, string elementsfilter, string referencefilter)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query GetDesigns ($designId: ID!, $filterQuery: String!, $name: String!, $query: String!, $includeReferencesProperties: String!){
                  elements (designId: $designId,
                    filter: { query: $elementsfilter }
                ) {
                       id
                       name
                       referencedBy(name: $name, filter: { query: $referencefilter}) {
                         results {
                           value {
                             properties(includeReferencesProperties: ""Type"") {
                               results {
                                 name
                                 value
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
                elementsfilter = elementsfilter,
                referencefilter = referencefilter
            }
        };

        return await Query(properties);
    }
}