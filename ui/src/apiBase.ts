// ClientOnly mode uses the Relay bridge as the data server.
// In dev (Vite on :5173), bridge runs on :3000.
// In production the bridge may serve the frontend on the same origin.
export const API_BASE = import.meta.env.DEV
  ? `http://${window.location.hostname}:3000`
  : window.location.origin;
