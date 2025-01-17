using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using System;

public partial class AECDMGraphQLController : ControllerBase
{
	[HttpGet("elementgroups/{elementGroupId}/schedule/{elementsfilter}")]
	public async Task<ActionResult<string>> GetSchedule(string elementGroupId, string elementsfilter, string? cursor, string? regionHeader)
	{
		var properties = new GraphQLRequest
		{
			Query = @"
			query GetSchedule($elementGroupId: ID!, $elementsfilter: String!){
				elementsByElementGroup(elementGroupId: $elementGroupId,filter: { query: $elementsfilter}){
					pagination{
						pageSize
						cursor
					}
					results{
						id
						name
						properties{
							results{
								name
								value
								displayValue
								definition{
									units{
										name
									}
								}
							}
						}
						references{
							results{
								name
								value{
									properties{
										results{
											name
											value
											displayValue
											definition{
												units{
													name
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}",
			Variables = new
			{
				elementGroupId = elementGroupId,
				elementsfilter = elementsfilter
			}
		};
		if (!String.IsNullOrWhiteSpace(cursor))
		{
			properties.Query = $@"
			query GetSchedule($elementGroupId: ID!, $elementsfilter: String!){{
				elementsByElementGroup(elementGroupId: $elementGroupId,filter: {{ query: $elementsfilter}}, pagination:{{cursor:""{cursor}""}}){{
					pagination{{
						pageSize
						cursor
					}}
					results{{
						id
						name
						properties{{
							results{{
								name
								value
								displayValue
								definition{{
									units{{
										name
									}}
								}}
							}}
						}}
						references{{
							results{{
								name
								value{{
									properties{{
										results{{
											name
											value
											displayValue
											definition{{
												units{{
													name
												}}
											}}
										}}
									}}
								}}
							}}
						}}
					}}
				}}
			}}";
		}

		return await Query(properties, regionHeader);
	}
}