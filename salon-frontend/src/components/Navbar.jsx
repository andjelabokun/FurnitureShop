import { Link } from "react-router-dom";

function Navbar() {
  return (
    <nav style={styles.nav}>
      <h2 style={styles.logo}>FurnitureShop</h2>

      <div style={styles.links}>
        <Link style={styles.link} to="/">
          Početna
        </Link>

        <Link style={styles.link} to="/products">
          Proizvodi
        </Link>

        <Link style={styles.link} to="/categories">
          Kategorije
        </Link>

        <Link style={styles.link} to="/about">
          O nama
        </Link>

        <Link style={styles.link} to="/admin">
          Admin
        </Link>
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
};

export default Navbar;