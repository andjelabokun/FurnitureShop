import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { useState, useEffect } from "react";
import { korpa } from "../pages/Cart";

function Navbar() {
  const { user, logout, isAdmin } = useAuth();
  const navigate = useNavigate();
  const [brojUKorpi, setBrojUKorpi] = useState(korpa.getItems().length);

  useEffect(() => {
    const unsub = korpa.subscribe(items => setBrojUKorpi(items.length));
    return unsub;
  }, []);

  const handleLogout = () => {
    logout();
    navigate('/');
  };

  return (
    <nav style={styles.nav}>
      <h2 style={styles.logo}>FurnitureShop</h2>

      <div style={styles.links}>
        <Link style={styles.link} to="/">Početna</Link>
        <Link style={styles.link} to="/products">Proizvodi</Link>
        <Link style={styles.link} to="/categories">Kategorije</Link>
        <Link style={styles.link} to="/about">O nama</Link>

        {isAdmin() && (
          <Link style={styles.link} to="/admin">Admin</Link>
        )}

        <Link style={styles.korpaLink} to="/cart">
          🛒 {brojUKorpi > 0 && <span style={styles.badge}>{brojUKorpi}</span>}
        </Link>

        {user ? (
          <>
            <span style={styles.ime}>Zdravo, {user.ime}!</span>
            <button style={styles.button} onClick={handleLogout}>Odjavi se</button>
          </>
        ) : (
          <>
            <Link style={styles.link} to="/login">Prijava</Link>
            <Link style={styles.linkButton} to="/register">Registracija</Link>
          </>
        )}
      </div>
    </nav>
  );
}

const styles = {
  nav: {
    width: "100vw",
    marginLeft: "calc(50% - 50vw)",
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    padding: "25px 60px",
    backgroundColor: "#cfe8ff",
    boxSizing: "border-box",
  },
  logo: {
    margin: 0,
    color: "#0b3d91",
    fontSize: "32px",
    fontWeight: "700",
  },
  links: {
    display: "flex",
    gap: "30px",
    alignItems: "center",
  },
  link: {
    textDecoration: "none",
    color: "#0b3d91",
    fontSize: "20px",
    fontWeight: "500",
  },
  linkButton: {
    textDecoration: "none",
    backgroundColor: "#0b3d91",
    color: "white",
    fontSize: "16px",
    fontWeight: "600",
    padding: "10px 20px",
    borderRadius: "10px",
  },
  korpaLink: {
    textDecoration: "none",
    fontSize: "24px",
    position: "relative",
    display: "flex",
    alignItems: "center",
  },
  badge: {
    position: "absolute",
    top: "-8px",
    right: "-8px",
    backgroundColor: "#dc2626",
    color: "white",
    borderRadius: "50%",
    width: "20px",
    height: "20px",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    fontSize: "12px",
    fontWeight: "700",
  },
  ime: {
    color: "#0b3d91",
    fontWeight: "600",
    fontSize: "16px",
  },
  button: {
    backgroundColor: "transparent",
    border: "2px solid #0b3d91",
    color: "#0b3d91",
    fontSize: "16px",
    fontWeight: "600",
    padding: "8px 18px",
    borderRadius: "10px",
    cursor: "pointer",
  },
};

export default Navbar;