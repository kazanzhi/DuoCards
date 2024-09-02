import { createBrowserRouter } from "react-router-dom";
import App from "../../App";
import { Profile } from "../../pages/Profile";
import { Home } from "../../pages/Home";
import { Card } from "../../pages/Card";
import { Learn } from "../../pages/Learn";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            { path: '/', element: <Home /> },
            { path: 'profile', element: <Profile /> },
            { path: 'card', element: <Card /> },
            { path: 'learn', element: <Learn/>}
            // path: '*'
        ]
    }
])