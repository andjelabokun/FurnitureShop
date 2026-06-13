import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

function Register() {
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [greska, setGreska] = useState('');
  const [uspeh, setUspeh] = useState('');
  const navigate = useNavigate();

  const handleRegister = async () => {
    try {
      await api.post('/Auth/register', { ime, prezime, email, password });
      setUspeh('Registracija uspešna! Preusmeravanje na prijavu...');
      setGreska('');
      setTimeout(() => navigate('/login'), 2000);
    } catch (err) {
      setGreska('Registracija nije uspela. Proverite podatke.');
      setUspeh('');
    }
  };

  return (
    <main style={styles.page}>
      <div style={styles.card}>
        <h1 style={styles.title}>Registracija</h1>
        <p style={styles.subtitle}>Kreirajte nalog i počnite sa kupovinom!</p>

        {greska && <p style={styles.greska}>{greska}</p>}
        {uspeh && <p style={styles.uspeh}>{uspeh}</p>}

        <input
          style={styles.input}
          type="text"
          placeholder="Ime"
          value={ime}
          onChange={e => setIme(e.target.value)}
        />
        <input
          style={styles.input}
          type="text"
          placeholder="Prezime"
          value={prezime}
          onChange={e => setPrezime(e.target.value)}
        />
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

        <button style={styles.button} onClick={handleRegister}>
          Registruj se
        </button>

        <p style={styles.link}>
          Već imate nalog?{' '}
          <a href="/login" style={styles.a}>Prijavite se</a>
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
  uspeh: {
    color: 'green',
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

export default Register;