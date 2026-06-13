import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import api from '../services/api';
import { jwtDecode } from 'jwt-decode';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [greska, setGreska] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await api.post('/Auth/login', { email, password });
      const token = response.data.token.result;
      const decoded = jwtDecode(token);
      console.log('Decoded token:', decoded);
      const role = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      const ime = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      const userId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']; 
      console.log('Role:', role);
      console.log('Ime:', ime);
      login(token, role, ime, userId); 
      if (role === 'Admin') navigate('/admin');
      else navigate('/products');
    } catch (err) {
      console.log('Greška:', err);
      setGreska('Pogrešan email ili lozinka.');
    }
};

  return (
    <main style={styles.page}>
      <div style={styles.card}>
        <h1 style={styles.title}>Prijava</h1>
        <p style={styles.subtitle}>Dobrodošli nazad!</p>

        {greska && <p style={styles.greska}>{greska}</p>}

        <input
          style={styles.input}
          type="email"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
        <input
          style={styles.input}
          type="password"
          placeholder="Lozinka"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />

        <button style={styles.button} onClick={handleLogin}>
          Prijavi se
        </button>

        <p style={styles.link}>
          Nemate nalog?{' '}
          <a href="/register" style={styles.a}>Registrujte se</a>
        </p>
      </div>
    </main>
  );
}

const styles = {
  page: {
    minHeight: '100vh',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    background: 'linear-gradient(180deg, #f7fbff 0%, #ffffff 100%)',
  },
  card: {
    backgroundColor: '#ffffff',
    borderRadius: '18px',
    padding: '50px',
    width: '100%',
    maxWidth: '420px',
    boxShadow: '0 10px 25px rgba(0,0,0,0.08)',
    display: 'flex',
    flexDirection: 'column',
    gap: '15px',
  },
  title: {
    fontSize: '32px',
    color: '#102a43',
    margin: 0,
  },
  subtitle: {
    color: '#627d98',
    margin: 0,
  },
  greska: {
    color: 'red',
    fontSize: '14px',
  },
  input: {
    padding: '14px',
    borderRadius: '10px',
    border: '1px solid #cfe8ff',
    fontSize: '16px',
    outline: 'none',
  },
  button: {
    padding: '14px',
    borderRadius: '10px',
    border: 'none',
    backgroundColor: '#0b3d91',
    color: 'white',
    fontSize: '16px',
    cursor: 'pointer',
    fontWeight: '600',
  },
  link: {
    textAlign: 'center',
    color: '#627d98',
  },
  a: {
    color: '#0b3d91',
    fontWeight: '600',
  },
};

export default Login;