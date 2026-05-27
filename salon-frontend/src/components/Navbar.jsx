function Navbar() {
  return (
    <nav style={styles.nav}>
      <h2 style={styles.logo}>FurnitureShop</h2>

      <div style={styles.links}>
        <a style={styles.link} href="/">Početna</a>
        <a style={styles.link} href="/products">Proizvodi</a>
        <a style={styles.link} href="/categories">Kategorije</a>
        <a style={styles.link} href="/admin">Admin</a>
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
  },

  links: {
    display: "flex",
    gap: "30px",
  },

  link: {
    textDecoration: "none",
    color: "#0b3d91",
    fontSize: "20px",
    fontWeight: "500",
  },
};

export default Navbar;