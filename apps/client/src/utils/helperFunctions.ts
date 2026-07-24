export const isValidJson = (item: any): boolean => {
	if (typeof item !== "string") return false;

	try {
		JSON.parse(item);
		return true;
	} catch (error) {
		return false;
	}
};

export const isValidEmail = (email: string) => {
	const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
	return regex.test(email);
};
