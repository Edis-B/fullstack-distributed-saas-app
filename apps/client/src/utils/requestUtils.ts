import { gatewayApi } from "@/common/constants";
import { isValidJson } from "./helperFunctions";

interface GetOptions extends RequestInit {
	queryParams?: Record<string, string>;
}

const request = async (
	url: string,
	method: string,
	config: RequestInit = {},
) => {
	return await fetch(url, {
		method,
		credentials: "include",
		...config,
	});
};

const gateway = {
	base: request,

	get: (url: string, getOptions?: GetOptions) => {
		const { queryParams, ...config } = getOptions || {};

		const searchParams = new URLSearchParams(queryParams || {});
		const queryString = searchParams.toString();
		const separator = queryString ? `?${queryString}` : "";

		return request(`${gatewayApi}${url}${separator}`, "GET", config);
	},

	post: (url: string, postOptions?: RequestInit) => {
		const { body, ...config } = postOptions || {};

		let finalBody = body;
		if (!isValidJson(finalBody)) {
			finalBody = JSON.stringify(finalBody);
		}

		return request(`${gatewayApi}${url}`, "POST", {
			body: finalBody,
			...config,
		});
	},
};

export { gateway };
