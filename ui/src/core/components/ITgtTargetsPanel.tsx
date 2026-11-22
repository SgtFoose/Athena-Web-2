/**
 * Right-hand "I-TGT TARGETS" panel (shared core, see docs/ui-core-sync.md).
 *
 * Lists the I-TGT targets the user stored from the map cursor (T key, middle
 * mouse button, or SAVE TGT on touch devices). Each target can be renamed,
 * copied (its 8-digit Firewill I-TGT code) or deleted. State lives in
 * `useITgtTargets`; this component only renders it.
 */
import type { StoredITgtTarget } from './AthenaMap'

interface ITgtTargetsPanelProps {
  /** Stored targets, newest first. */
  targets: StoredITgtTarget[]
  /** True on touch devices; changes the usage hint. */
  isTouchInput: boolean
  onRename: (id: string, nextLabel: string) => void
  onDelete: (id: string) => void
  /** Clears all targets and restarts labels at TGT_0. */
  onDeleteAll: () => void
  /** Copies an I-TGT code to the clipboard. */
  onCopy: (code: string) => void | Promise<void>
}

export function ITgtTargetsPanel({ targets, isTouchInput, onRename, onDelete, onDeleteAll, onCopy }: ITgtTargetsPanelProps) {
  return (
    <>
      <div className="panel-header">I-TGT TARGETS</div>
      <div style={{ padding: '10px 8px', display: 'flex', flexDirection: 'column', gap: 8, fontSize: 12 }}>
        <div style={{ color: '#8FA7C8', lineHeight: 1.45 }}>
          {isTouchInput
            ? <>Touch mode: tap map to set cursor, then use <strong>SAVE TGT</strong> on the map overlay.</>
            : <>Store current mouse map position: <strong>T</strong> or <strong>middle mouse button</strong>.</>}
        </div>

        {targets.length > 0 && (
          <button
            type="button"
            onClick={onDeleteAll}
            style={{
              alignSelf: 'flex-start',
              border: '1px solid #6E3B49',
              borderRadius: 4,
              background: '#2B1319',
              color: '#FFB9C7',
              cursor: 'pointer',
              fontSize: 11,
              fontWeight: 700,
              padding: '4px 8px',
            }}
          >
            Delete All
          </button>
        )}

        {targets.length === 0 ? (
          <div style={{ color: '#7A8796', fontSize: 12 }}>No stored I-TGT coordinates yet.</div>
        ) : (
          <div style={{ display: 'flex', flexDirection: 'column', gap: 6, maxHeight: 'calc(100vh - 180px)', overflowY: 'auto', paddingRight: 2 }}>
            {targets.map(target => (
              <div
                key={target.id}
                style={{
                  border: '1px solid #2C3C53',
                  borderRadius: 6,
                  background: 'rgba(11, 17, 30, 0.75)',
                  padding: 6,
                  display: 'flex',
                  flexDirection: 'column',
                  gap: 6,
                }}
              >
                <div style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
                  <input
                    value={target.label}
                    onChange={(e) => onRename(target.id, e.target.value)}
                    style={{
                      flex: 1,
                      minWidth: 0,
                      background: '#101827',
                      border: '1px solid #364963',
                      color: '#DDE9FF',
                      borderRadius: 4,
                      fontSize: 12,
                      padding: '4px 6px',
                    }}
                  />
                  <button
                    type="button"
                    title="Copy I-TGT code"
                    onClick={() => void onCopy(target.code)}
                    style={{
                      width: 24,
                      height: 24,
                      border: '1px solid #4D6382',
                      borderRadius: 4,
                      background: '#142239',
                      color: '#DDE9FF',
                      cursor: 'pointer',
                    }}
                  >
                    ⧉
                  </button>
                  <button
                    type="button"
                    title="Delete target"
                    onClick={() => onDelete(target.id)}
                    style={{
                      width: 24,
                      height: 24,
                      border: '1px solid #6E3B49',
                      borderRadius: 4,
                      background: '#2B1319',
                      color: '#FFB9C7',
                      cursor: 'pointer',
                    }}
                  >
                    ✕
                  </button>
                </div>
                <div style={{ color: '#FFCD7E', fontFamily: 'Consolas, "Lucida Console", monospace', fontSize: 14, fontWeight: 700 }}>
                  {target.code}
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  )
}
