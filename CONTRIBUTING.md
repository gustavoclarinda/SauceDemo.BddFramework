# Contributing Guidelines

## Project layout standard
This repository follows a consistent, professional folder structure. When adding or moving projects, follow these conventions:

- `src/` - All production projects (class libraries, apps) live here. Each project gets its own subfolder named after the project, e.g. `src/SauceDemo.BddFramework/`.
- `tests/` - All test projects (unit, integration, BDD) live here. Each test project gets its own subfolder, e.g. `tests/SauceDemo.BddFramework.Tests/`.
- `tools/` - Local tools, scripts, or CLI installers used by CI.
- `docs/` - Documentation and generated reports (optional).
- Root files that apply to the entire repository (solution file, README.md, .editorconfig, specflow.json) remain at repository root.
- `TestResults/` - CI test run outputs (TRX/JSON) are written here (repository root).
- `LivingDoc/` - LivingDoc HTML output lives here (repository root) and is published to GitHub Pages.

Example tree:

```
/ (repo root)
  README.md
  specflow.json
  SauceDemo.sln
  /src
    /SauceDemo.BddFramework
      SauceDemo.BddFramework.csproj
      ...source files...
  /tests
    /SauceDemo.BddFramework.Specs
      SauceDemo.BddFramework.Specs.csproj
      ...test files...
  /tools
  /docs
  /TestResults
  /LivingDoc
  .github/
```

## Naming and references
- Project directories should use PascalCase matching the project/assembly name.
- Keep project references (ProjectReference) relative and consistent. After moving files, update solution and project references using `dotnet sln` and `dotnet add <project> reference`.

## CI and test artifacts
- CI workflows must reference projects by their new paths under `src/` and write test outputs to the repository-level `TestResults/` directory.
- The `livingdoc` CLI expects a compiled test assembly DLL and a SpecFlow JSON (preferred) or TRX file. Configure `dotnet test` to produce a SpecFlow JSON using the appropriate logger (e.g., `SpecFlow+LivingDoc`), and ensure the `livingdoc` step points to the correct DLL path under `src/<project>/bin/Release/net8.0/` and the JSON/TRX under `TestResults/`.

## Steps to move and update projects (recommended commands)
Run the following from the repository root. Review each command output and confirm changes locally before pushing.

- Create directories:

```bash
mkdir -p src tests tools docs
```

- Move a project into `src/` (example):

```bash
git mv SauceDemo.BddFramework.csproj src/SauceDemo.BddFramework/
# Move all project source files into the new folder
git mv *.cs src/SauceDemo.BddFramework/ || true
git mv Properties src/SauceDemo.BddFramework/ || true
```

- If you have a solution file, update it:

```bash
# Remove old project from solution (if it was added from root)
dotnet sln remove SauceDemo.BddFramework.csproj || true
# Add project from new path
dotnet sln add src/SauceDemo.BddFramework/SauceDemo.BddFramework.csproj
```

- Update ProjectReference paths inside `.csproj` files if necessary. You can open the `.csproj` and change the `Include` to the new relative path.

- Move test projects into `tests/` and add to solution similarly.

- Create (or ensure) repository-level `TestResults` directory:

```bash
mkdir -p TestResults
```

- Commit the reorganized files:

```bash
git add -A
git commit -m "Move projects to src/ and tests/; update solution and CI paths"
```

## CI adjustments
When updating GitHub Actions workflows (for Windows runners in this repository), ensure the steps use the new project paths and that `TestResults` and `LivingDoc` paths are used as repository-level outputs.

Example changes to `.github/workflows/ci-tests.yml` will include:
- Building `src/<project>/<project>.csproj`
- Running `dotnet test src/<project>/<project>.csproj` with logger output to `TestResults/`
- Passing the generated DLL path from `src/<project>/bin/Release/net8.0/<project>.dll` to `livingdoc` and pointing the `-t` option to the JSON or TRX file in `TestResults/`.

## Validation
After changes:
- Run `dotnet build` and `dotnet test` locally using the new paths.
- Confirm `TestResults/*.json` or `TestResults/*.trx` are produced.
- Confirm `livingdoc test-assembly` generates `LivingDoc/index.html`.

## Troubleshooting
- If `livingdoc` fails with a NullReferenceException, ensure you are providing a SpecFlow JSON produced by the correct logger. Use `dotnet test --list-loggers` to check available loggers.
- If project references are broken, open the `.csproj` files and inspect `ProjectReference` `Include` paths.

## Formatting and editor config
- Keep an `.editorconfig` at the repository root to enforce code style rules across the repo.

---

Following these standards will make the repository easier to maintain and follow CI best practices.