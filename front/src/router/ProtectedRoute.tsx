import React from 'react'
import { useLocation, Navigate } from 'react-router-dom';

type ProtectedRouteProps = {
    children: JSX.Element;
};

export const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
    const token = localStorage.getItem('token')
    const location = useLocation()

    if (!token) {
        return <Navigate to="/login" state={{ from: location }} />;
    }

    return children;
}