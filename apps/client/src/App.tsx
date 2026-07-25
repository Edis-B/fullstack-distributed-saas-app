import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import { AuthProvider } from "./context/AuthContext";
import Dashboard from "./components/Dashboard";

function App() {
	const router = createBrowserRouter([
		{
			path: "/",
			element: <Dashboard />,
		},
		{
			path: "/login",
			element: <Login />,
		},
		{
			path: "/register",
			element: <Register />,
		},
	]);

	return (
		<AuthProvider>
			<RouterProvider router={router} />;
		</AuthProvider>
	);
}

export default App;
