import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import { Profile } from "../pages/Profile/Profile";
import { Home } from "../pages/Home/Home";
import { CreateCard } from "../pages/CreateCard/CreateCard";
import { Learn } from "../pages/Learn/Learn";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '/', element: <Home /> },
            { path: 'profile', element: <Profile /> },
            { path: 'card', element: <CreateCard /> },
            { path: 'learn', element: <Learn /> }
            // path: '*'
        ]
    }
])