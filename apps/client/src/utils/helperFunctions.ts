export const isValidJson = (item: any): boolean => {
	if (typeof item !== "string") return false;

	try {
		JSON.parse(item);
		return true;
	} catch (error) {
		return false;
	}
};
