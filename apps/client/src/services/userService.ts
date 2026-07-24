import { isValidEmail } from "@/utils/helperFunctions";
import { gateway } from "@/utils/requestUtils";

export async function loginUserAsync(
	usernameOrEmail: string,
	password: string,
) {
	const response = await gateway.post("/api/users/login", {
		body: {
			usernameOrEmail,
			password,
		},
	});

	console.log(response);
	console.log(await response.json());
}

export async function registerUserAsync(
	username: string,
	email: string,
	password: string,
) {
	const response = await gateway.post("/api/users/register", {
		body: {
			username,
			email,
			password,
		},
	});
}
