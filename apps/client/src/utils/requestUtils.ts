import { gatewayApi } from "@/common/constants";
import { isValidJson } from "./helperFunctions";

interface GetOptions extends RequestInit {
	queryParams?: Record<string, string>;
}

interface PostOptions extends Omit<RequestInit, "body"> {
	body?: any;
}

const request = async (
	url: string,
	method: string,
	config: RequestInit = {},
) => {
	const options: RequestInit = {
		method,
		credentials: "include",
		...config,
	};

	return fetch(url, options);
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

	post: (url: string, postOptions?: PostOptions) => {
		const { body, headers, ...config } = postOptions || {};

		let finalBody = body;

		const finalHeaders = new Headers(headers);

		if (finalBody && !isValidJson(finalBody)) {
			finalBody = JSON.stringify(finalBody);

			if (!finalHeaders.has("Content-Type")) {
				finalHeaders.set("Content-Type", "application/json");
			}
		}

		return request(`${gatewayApi}${url}`, "POST", {
			body: finalBody,
			headers: finalHeaders,
			...config,
		});
	},
};

export { gateway };
