/**
 * Top of the left sidebar: app name, version and donate link
 * (shared core, see docs/ui-core-sync.md). All three values come from the
 * app's own `appConfig.ts`, so each app shows its own branding.
 */
import { APP_NAME, APP_VERSION, DONATE_URL } from '../../appConfig'

export function SidebarHeader() {
  return (
    <div className="sidebar-header">
      <span className="logo-text">{APP_NAME.toUpperCase()}</span>
      <span className="header-right">
        <span className="version-label">v{APP_VERSION}</span>
        <a
          className="donate-link"
          href={DONATE_URL}
          target="_blank"
          rel="noreferrer"
          title="Support development"
        >Donate</a>
      </span>
    </div>
  )
}
