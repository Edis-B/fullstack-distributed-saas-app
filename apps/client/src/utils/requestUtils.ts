import { gatewayApi } from "@/common/constants";

const request = async (url: string, method: string, options: {} = {}) => {
	return await fetch(url, {
		method,
		...options,
	});
};

const gateway = {
	base: request,
	get: (url: string, options?: {}) =>
		request(`${gatewayApi}${url}`, "GET", options),
	post: (url: string, options?: {}) =>
		request(`${gatewayApi}${url}`, "POST", options),
};

export { gateway };
