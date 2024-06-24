using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
    [HttpGet("elementgroups/{elementGroupId}/takeoff/{elementsfilter}")]
    public async Task<ActionResult<string>> GetQuantityTakeOff(string elementGroupId, string elementsfilter, string? cursor)
    {
        var properties = new GraphQLRequest
        {
            Query = @"
            query getQuantityTakeoff ($elementGroupId: ID!, $elementsfilter: String!){
                elementsByElementGroup(elementGroupId: $elementGroupId, filter: { query: $elementsfilter}) {
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
                elementGroupId = elementGroupId,
                elementsfilter = elementsfilter
            }
        };
        if (!String.IsNullOrEmpty(cursor))
        {
            properties.Query = $@"
            query getQuantityTakeoff ($elementGroupId: ID!, $elementsfilter: String!){{
                elementsByElementGroup(elementGroupId: $elementGroupId, filter: {{ query: $elementsfilter}}, pagination:{{cursor:""{cursor}""}}) {{
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