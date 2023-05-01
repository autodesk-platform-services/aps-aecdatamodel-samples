using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/procurement")]
    public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, string elementsfilter, string name, string referencefilter)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
              query GetProcurement ($designId: ID!, $elementsfilter: String!, $name: String!, $referencefilter: String!){
                  elements (designId: $designId,filter: { query: $elementsfilter }){
		results{
			id
			name
			referencedBy(name: $name, filter:{query:$referencefilter}){
				results{
					id
					name
					properties(includeReferencesProperties: ""Type""){
						results{
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
								name = name,
                referencefilter = referencefilter
            }
        };

        return await Query(properties);
    }
}