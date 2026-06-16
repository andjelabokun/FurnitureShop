import { useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { jwtDecode } from 'jwt-decode';

function GoogleAuth() {
  const [searchParams] = useSearchParams();
  const { login } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    const token = searchParams.get('token');
    if (token) {
      const decoded = jwtDecode(token);
      const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      const ime = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      const userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      login(token, role, ime, userId);
      if (role === 'Admin') navigate('/admin');
      else navigate('/products');
    }
  }, []);

  return <p>Prijavljivanje...</p>;
}

export default GoogleAuth;