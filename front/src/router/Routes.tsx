import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import { Home } from "../pages/Home/Home";
import { CreateCard } from "../pages/CreateCard/CreateCard";
import { Learn } from "../pages/Learn/Learn";
import LoginSignup from "../pages/LoginSignup/LoginSignup";
import { ProtectedRoute } from "./ProtectedRoute";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {
                path: '/',
                element: (
                    <ProtectedRoute>
                        <Home />
                    </ProtectedRoute>
                )
            },
            {
                path: 'create',
                element: (
                    <ProtectedRoute>
                        <CreateCard />
                    </ProtectedRoute>
                )
            },
            {
                path: 'learn',
                element: (
                    <ProtectedRoute>
                        <Learn />
                    </ProtectedRoute>
                )
            },
            {
                path: 'edit/:id',
                element: (
                    <ProtectedRoute>
                        <CreateCard />
                    </ProtectedRoute>
                )
            },
            { path: 'login', element: <LoginSignup /> },
            { path: 'signup', element: <LoginSignup /> },
        ]
    }
])