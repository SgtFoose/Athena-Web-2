/**
 * Top of the left sidebar: app name, version and donate link
 * (shared core, see docs/ui-core-sync.md). All three values come from the
 * app's own `appConfig.ts`, so each app shows its own branding. Test builds
 * show the build label (e.g. "v1.2.0-test (a1b2c3d)"); hover for the build time.
 */
import { APP_NAME, DONATE_URL } from '../../appConfig'
import { APP_VERSION_LABEL, APP_VERSION_SHORT_LABEL, APP_BUILD_TOOLTIP } from '../utils/version'

export function SidebarHeader() {
  return (
    <div className="sidebar-header">
      <span className="logo-text">{APP_NAME.toUpperCase()}</span>
      <span className="header-right">
        <span className="version-label" title={`${APP_VERSION_LABEL} - ${APP_BUILD_TOOLTIP}`}>{APP_VERSION_SHORT_LABEL}</span>
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
