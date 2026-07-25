import { useAuth } from "@/context/AuthContext";

export default function Dashboard() {
	const { user } = useAuth();

	if (user != null) {
		return <div>{user.username}</div>;
	}

	return <div>Test</div>;
}
