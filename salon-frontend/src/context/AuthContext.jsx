import { createContext, useContext, useState } from 'react';
import { korpa } from '../pages/Cart';

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('role');
    const ime = localStorage.getItem('ime');
    const userId = localStorage.getItem('userId'); 
    return token ? { token, role, ime, userId } : null; 
});

  const login = (token, role, ime, userId) => {
    localStorage.setItem('token', token);
    localStorage.setItem('role', role);
    localStorage.setItem('ime', ime);
    localStorage.setItem('userId', userId); 
    setUser({ token, role, ime, userId }); 
};



const logout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('ime');
    localStorage.removeItem('userId');
    korpa.clear();  
    setUser(null);
};

  const isAdmin = () => {
    if (!user) return false;
    if (Array.isArray(user.role)) return user.role.includes('Admin');
    return user.role === 'Admin';
};
  const isKupac = () => user?.role === 'Kupac';
  const isLoggedIn = () => !!user;

  return (
    <AuthContext.Provider value={{ user, login, logout, isAdmin, isKupac, isLoggedIn }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}