import {
	Card,
	CardHeader,
	CardTitle,
	CardDescription,
	CardContent,
	CardFooter,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import { Link } from "react-router-dom";
import { useState } from "react";
import { AuthService } from "@/api/generated/users";

export default function Login() {
	const [usernameOrEmail, setUsernameOrEmail] = useState("");
	const [password, setPassword] = useState("");

	const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();

		try {
			const response = await AuthService.postLogin({
				usernameOrEmail,
				password,
				rememberMe: false,
			});

			console.log("Login successful!", response);
		} catch (error) {
			console.error("Login failed:", error);
		}
	};

	return (
		<div className="flex items-center justify-center min-h-screen bg-gray-50">
			<Card className="w-[350px]">
				<CardHeader>
					<CardTitle>Sign In</CardTitle>
					<CardDescription>
						Enter your Username/Email and password to access your
						account.
					</CardDescription>
				</CardHeader>

				<CardContent className="space-y-4">
					<div className="space-y-2">
						<Label htmlFor="usernameOrEmail">Email/Username</Label>
						<Input
							id="usernameOrEmail"
							type="usernameOrEmail"
							placeholder="name@example.com"
							value={usernameOrEmail}
							onChange={(e) => setUsernameOrEmail(e.target.value)}
						/>
					</div>
					<div className="space-y-2">
						<Label htmlFor="password">Password</Label>
						<Input
							id="password"
							type="password"
							value={password}
							onChange={(e) => setPassword(e.target.value)}
						/>
					</div>
				</CardContent>

				<form onSubmit={handleSubmit}>
					<CardFooter className="flex flex-col gap-4">
						<Button className="w-full" type="submit">
							Sign In
						</Button>
						<Link
							to="/register"
							className="text-sm text-muted-foreground hover:underline hover:text-primary"
						>
							Don't have an account?
						</Link>
					</CardFooter>
				</form>
			</Card>
		</div>
	);
}
