import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import { Home } from "../pages/Home/Home";
import { CreateCard } from "../pages/CreateCard/CreateCard";
import { Learn } from "../pages/Learn/Learn";
import LoginSignup from "../pages/LoginSignup/LoginSignup";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '/', element: <Home /> },
            { path: 'create', element: <CreateCard /> },
            { path: 'learn', element: <Learn /> },
            { path: 'login', element: <LoginSignup /> },
            { path: 'signup', element: <LoginSignup /> },
            { path: 'edit/:id', element: <CreateCard /> }
            // path: '*'
        ]
    }
])