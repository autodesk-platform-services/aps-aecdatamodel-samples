using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECCIMGraphQLController : ControllerBase
{
    [HttpGet("designs/{designId}/takeoff/{elementsfilter}")]
    public async Task<ActionResult<string>> GetQuantityTakeOff(string designId, string elementsfilter, string? cursor)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
            query getQuantityTakeoff ($designId: ID!, $elementsfilter: String!){
                elements (designId: $designId, filter: { query: $elementsfilter}) {
                    pagination{
                        pageSize
                        cursor
                    }
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
        if (!String.IsNullOrEmpty(cursor))
        {
            properties.Query = $@"
            query getQuantityTakeoff ($designId: ID!, $elementsfilter: String!){{
                elements (designId: $designId, filter: {{ query: $elementsfilter}}, pagination:{{cursor:""{cursor}""}}) {{
                    pagination{{
                        pageSize
                        cursor
                    }}
                    results{{
                        id
                        name
                    }}
                }}
            }}";
        }

        return await Query(properties);
    }
}