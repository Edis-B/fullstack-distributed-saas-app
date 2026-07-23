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

export default function Login() {
	return (
		<div className="flex items-center justify-center min-h-screen bg-gray-50">
			<Card className="w-[350px]">
				<CardHeader>
					<CardTitle>Sign In</CardTitle>
					<CardDescription>
						Enter your email and password to access your account.
					</CardDescription>
				</CardHeader>

				<CardContent className="space-y-4">
					<div className="space-y-2">
						<Label htmlFor="email">Email</Label>
						<Input
							id="email"
							type="email"
							placeholder="name@example.com"
						/>
					</div>
					<div className="space-y-2">
						<Label htmlFor="password">Password</Label>
						<Input id="password" type="password" />
					</div>
				</CardContent>

				<CardFooter className="flex flex-col gap-4">
					<Button className="w-full">Sign In</Button>
					<Link
						to="/register"
						className="text-sm text-muted-foreground hover:underline hover:text-primary"
					>
						Don't have an account?
					</Link>
				</CardFooter>
			</Card>
		</div>
	);
}
