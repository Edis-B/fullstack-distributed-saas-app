import { AuthService } from "@/api/generated/users";
import {
	Children,
	createContext,
	useContext,
	useEffect,
	useState,
	type ReactNode,
} from "react";

interface AuthProviderProps {
	children: ReactNode;
}

interface User {
	username: string | null;
	email: string | null;
}

interface AuthContextType {
	user: User | null;
	logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: AuthProviderProps) {
	const [user, setUser] = useState<User | null>(null);

	const logout = () => setUser(null);

	useEffect(() => {
		const fetchStatus = async () => {
			try {
				const response = await AuthService.getStatus();

				setUser({
					username: response.username ?? null,
					email: response.email ?? null,
				});
			} catch {
				console.log("Not logged in!");
			}
		};

		fetchStatus();
	}, []);

	return (
		<AuthContext.Provider value={{ user, logout }}>
			{children}
		</AuthContext.Provider>
	);
}

export const useAuth = () => {
	const context = useContext(AuthContext);

	if (context === undefined) {
		throw new Error("useAuth must be used within an AuthProvider");
	}

	return context;
};
