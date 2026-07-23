import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { gateway } from "./utils/requestUtils";
import { Card } from "./components/ui/card";

function App() {
	// 1. Create a state variable to hold your data once it arrives
	const [result2, setResult2] = useState<string | null>(null);

	// 2. Use useEffect to run the async code when the component mounts
	useEffect(() => {
		// You must declare an async function INSIDE the useEffect
		const fetchData = async () => {
			try {
				const response = await gateway.get("/api/users/values");

				// Assuming response has a .json() method or returns raw data.
				// Adjust this line based on how your 'api' wrapper is structured.
				const data = await response.json();

				setResult2(JSON.stringify(data)); // Save the data to state
			} catch (error) {
				console.error("Error fetching data:", error);
				setResult2("Error loading data");
			}
		};

		// Call the function immediately
		fetchData();
	}, []); // The empty array means this only runs once on load

	return (
		<div className="p-8">
			<Button>Click Me</Button>
			<div className="mt-4">
				{/* 3. Render the state. It will be null briefly while awaiting. */}
				{result2 ? result2 : "Loading..."}
			</div>

			<Card></Card>
		</div>
	);
}

export default App;
