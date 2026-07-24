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
import { useEffect, useState, type ReactEventHandler } from "react";
import { gatewayApi } from "@/common/constants";
import { gateway } from "@/utils/requestUtils";
import { registerUserAsync } from "@/services/userService";

export default function Register() {
	const [username, setUsername] = useState("");
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");

	const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();

		await registerUserAsync(username, email, password);
	};

	return (
		<div className="flex items-center justify-center min-h-screen bg-gray-50">
			<Card className="w-[350px]">
				<CardHeader>
					<CardTitle>Register</CardTitle>
					<CardDescription>
						Enter credentials to create an account
					</CardDescription>
				</CardHeader>

				<CardContent className="space-y-4">
					<div className="space-y-2">
						<Label htmlFor="email">Email</Label>
						<Input
							id="email"
							type="email"
							placeholder="name@example.com"
							value={email}
							onChange={(e) => setEmail(e.target.value)}
						/>
					</div>
					<div className="space-y-2">
						<Label htmlFor="email">Username</Label>
						<Input
							id="username"
							type="text"
							placeholder="John Doe"
							value={username}
							onChange={(e) => setUsername(e.target.value)}
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
							Register
						</Button>
						<Link
							to="/login"
							className="text-sm text-muted-foreground hover:underline hover:text-primary"
						>
							Already have an account?
						</Link>
					</CardFooter>
				</form>
			</Card>
		</div>
	);
}
