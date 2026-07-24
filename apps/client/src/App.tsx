import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Login from "./components/Login";
import Register from "./components/Register";

function App() {
	const router = createBrowserRouter([
		{
			path: "/",
			element: <div>Dashboard Page</div>,
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

	return <RouterProvider router={router} />;
}

export default App;
