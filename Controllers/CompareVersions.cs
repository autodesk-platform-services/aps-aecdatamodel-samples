using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("/api/graphql/designs/{designId}/version/{versionNumber}")]
    public async Task<ActionResult<string>> GetFurnitureProcurement(string designId, int versionNumber)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
            query getVersionProperties($designId: ID!, $versionNumber: Int!){
              aecDesignByVersionNumber(designId:$designId , versionNumber:$versionNumber ){
		name
		elements{
			results{
				id
				name
				properties{
					results{
						name
						value
						propertyDefinition{
							id
							name
							units
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