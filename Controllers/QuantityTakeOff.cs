using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/takeoff/{elementsfilter}")]
    public async Task<ActionResult<string>> GetQuantityTakeOff(string designId, string elementsfilter)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
                query getQuantityTakeoff ($designId: ID!, $elementsfilter: String!){
	elements (designId: $designId, filter: { query: $elementsfilter}) {
			results{
				id
				name
			 }
		}
}",
            Variables = new
            {
                designId = designId,
                elementsfilter = elementsfilter
            }
        };

        return await Query(properties);
    }
}