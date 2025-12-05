#!/usr/bin/env bash
set -euo pipefail

# Bootstrap remotes for the ERP workspace repos.
# Usage:
#   GH_OWNER=my-org VISIBILITY=private AUTO_COMMIT=false ./scripts/bootstrap-remotes.sh
# - GH_OWNER: required GitHub username or org.
# - VISIBILITY: "private" (default) or "public" for `gh repo create`.
# - AUTO_COMMIT: if "true", will auto-commit and push initial contents after wiring remotes.
#   Leave as "false" to inspect manually.

WORKSPACE_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
REPOS=(
  erp-vibe-spec
  erp-backend
  erp-admin-frontend
  erp-tenant-frontend-template
  erp-infra-admin-backend
  erp-infra-tenant-frontend
)

GH_OWNER="${GH_OWNER:-}"
VISIBILITY="${VISIBILITY:-private}"
AUTO_COMMIT="${AUTO_COMMIT:-false}"

if [[ -z "${GH_OWNER}" ]]; then
  echo "[bootstrap] GH_OWNER is required (export GH_OWNER=your-org-or-user)" >&2
  exit 1
fi

git_version=$(git --version 2>/dev/null || true)
if [[ -z "${git_version}" ]]; then
  echo "[bootstrap] git is required in the PATH" >&2
  exit 1
fi

create_remote_with_gh() {
  local repo_path="$1"
  local repo_name="$2"
  gh repo create "${GH_OWNER}/${repo_name}" --"${VISIBILITY}" --source="${repo_path}" --remote=origin --push --branch=main
}

create_remote_manual() {
  local repo_path="$1"
  local repo_name="$2"
  git -C "${repo_path}" remote add origin "git@github.com:${GH_OWNER}/${repo_name}.git"
  echo "[bootstrap] Added manual origin for ${repo_name}. Run:\n  (cd ${repo_path} && git push -u origin main)" >&2
}

for repo in "${REPOS[@]}"; do
  repo_path="${WORKSPACE_ROOT}/${repo}"
  if [[ ! -d "${repo_path}" ]]; then
    echo "[bootstrap] Skipping ${repo}: path not found" >&2
    continue
  fi

  if [[ ! -d "${repo_path}/.git" ]]; then
    echo "[bootstrap] Initializing git in ${repo}" >&2
    git -C "${repo_path}" init
    git -C "${repo_path}" checkout -B main
  fi

  if ! git -C "${repo_path}" remote | grep -q '^origin$'; then
    if command -v gh >/dev/null 2>&1; then
      echo "[bootstrap] Creating GitHub repo ${GH_OWNER}/${repo} via gh" >&2
      create_remote_with_gh "${repo_path}" "${repo}"
    else
      echo "[bootstrap] gh not available; wiring manual origin for ${repo}" >&2
      create_remote_manual "${repo_path}" "${repo}"
    fi
  else
    echo "[bootstrap] Origin already configured for ${repo}" >&2
  fi

  if [[ "${AUTO_COMMIT}" == "true" ]]; then
    echo "[bootstrap] Auto-committing and pushing ${repo}" >&2
    git -C "${repo_path}" add .
    git -C "${repo_path}" commit -m "chore: initial import" || true
    git -C "${repo_path}" push -u origin main || true
  else
    echo "[bootstrap] Auto-commit disabled; review 'git status' inside ${repo} before pushing" >&2
  fi

done
